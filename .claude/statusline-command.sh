#!/usr/bin/env bash
# Claude Code status line — consommation tokens en temps réel
# Budget hebdomadaire Pro (ajuste selon ta consommation réelle)
WEEKLY_BUDGET=500000

INPUT=$(cat)
TODAY=$(date +%Y-%m-%d)
WEEK=$(date +%Y-W%V)
USAGE_FILE="${HOME}/.claude/usage_stats.json"
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

RESULT=$(echo "$INPUT" | python3 "$SCRIPT_DIR/statusline-parse.py" "$TODAY" "$WEEK" "$USAGE_FILE" 2>/dev/null)

# used_pct|model|cwd|daily|weekly|last_5h|next_5h
IFS='|' read -r used_pct model cwd daily_tokens weekly_tokens last_5h next_5h <<< "$RESULT"

# Format: 1200 → 1.2k, 1500000 → 1.5M
fmt_k() {
  local n=${1:-0}
  if [ "$n" -ge 1000000 ] 2>/dev/null; then
    awk "BEGIN {printf \"%.1fM\", $n/1000000}"
  elif [ "$n" -ge 1000 ] 2>/dev/null; then
    awk "BEGIN {printf \"%.1fk\", $n/1000}"
  else
    printf "%s" "$n"
  fi
}

daily_fmt=$(fmt_k "${daily_tokens:-0}")
last5h_fmt=$(fmt_k "${last_5h:-0}")
next5h_fmt=$(fmt_k "${next_5h:-0}")

# % semaine par rapport au budget
weekly_fmt=$(fmt_k "${weekly_tokens:-0}")
weekly_pct=""
if [ "${weekly_tokens:-0}" -gt 0 ] && [ "$WEEKLY_BUDGET" -gt 0 ] 2>/dev/null; then
  weekly_pct=$(awk "BEGIN {printf \"%.0f\", ${weekly_tokens:-0} * 100 / $WEEKLY_BUDGET}")
  weekly_fmt="${weekly_fmt} (${weekly_pct}%)"
fi

# Context window %
ctx_part=""
if [ -n "$used_pct" ] && [ "$used_pct" != "None" ]; then
  ctx_part=" | ctx: ${used_pct}%"
fi

# Basename du répertoire courant
dir_display=""
if [ -n "$cwd" ]; then
  dir_display=" $(basename "$cwd")"
fi

printf "%s%s | jour: %s | 5h: %s→%s | sem: %s%s\n" \
  "${model:-Claude}" \
  "$dir_display" \
  "$daily_fmt" \
  "$last5h_fmt" \
  "$next5h_fmt" \
  "$weekly_fmt" \
  "$ctx_part"
