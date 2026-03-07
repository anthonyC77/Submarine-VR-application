import json, sys, os
from datetime import datetime, timezone

today      = sys.argv[1]
week       = sys.argv[2]
usage_file = sys.argv[3]

raw = sys.stdin.read().strip()
try:
    j = json.loads(raw)
except Exception:
    j = {}

ctx        = j.get('context_window', {})
used_pct   = str(ctx.get('used_percentage', ''))
total_in   = int(ctx.get('total_input_tokens', 0))
total_out  = int(ctx.get('total_output_tokens', 0))
model      = j.get('model', {}).get('display_name', 'Claude')
cwd        = j.get('workspace', {}).get('current_dir', j.get('cwd', ''))
session_id = j.get('session_id', '')

now_ts = datetime.now(timezone.utc).timestamp()

# Load or init usage stats
stats = {'sessions': {}}
if os.path.exists(usage_file):
    try:
        with open(usage_file) as f:
            stats = json.load(f)
    except Exception:
        pass

# Update this session
if session_id:
    stats['sessions'][session_id] = {
        'date':         today,
        'week':         week,
        'total_tokens': total_in + total_out,
        'input_tokens': total_in,
        'output_tokens': total_out,
        'last_update':  now_ts
    }
    try:
        os.makedirs(os.path.dirname(usage_file), exist_ok=True)
        with open(usage_file, 'w') as f:
            json.dump(stats, f)
    except Exception:
        pass

sessions = stats['sessions'].values()

# Daily & weekly totals
daily  = sum(s['total_tokens'] for s in sessions if s.get('date') == today)
weekly = sum(s['total_tokens'] for s in sessions if s.get('week') == week)

# Last 5 hours
cutoff_5h = now_ts - 5 * 3600
last_5h = sum(
    s['total_tokens'] for s in sessions
    if s.get('last_update', 0) >= cutoff_5h
)

# Projected next 5 hours based on today's burn rate
start_of_day = datetime.now(timezone.utc).replace(hour=0, minute=0, second=0, microsecond=0).timestamp()
hours_elapsed = max((now_ts - start_of_day) / 3600, 0.1)
hourly_rate = daily / hours_elapsed
next_5h = int(hourly_rate * 5)

print(f"{used_pct}|{model}|{cwd}|{daily}|{weekly}|{last_5h}|{next_5h}")
