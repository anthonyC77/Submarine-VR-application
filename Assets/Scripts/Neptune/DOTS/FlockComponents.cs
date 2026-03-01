using Unity.Entities;
using Unity.Mathematics;

namespace Neptune.DOTS
{
    // ============ COMPONENTS POUR LES POISSONS ============

    /// <summary>
    /// Données de mouvement du poisson
    /// </summary>
    public struct FlockVelocity : IComponentData
    {
        public float3 Value;
    }

    /// <summary>
    /// Vitesse actuelle du poisson
    /// </summary>
    public struct FlockSpeed : IComponentData
    {
        public float Value;
    }

    /// <summary>
    /// État du poisson (flags pour Turning, TurningCollision)
    /// </summary>
    public struct FlockState : IComponentData
    {
        public bool Turning;
        public bool TurningCollision;
        public float3 GoalPosition;
    }

    /// <summary>
    /// Rayon de détection des collisions
    /// </summary>
    public struct FlockCollisionRadius : IComponentData
    {
        public float Value;
    }

    /// <summary>
    /// Référence au pool de paramètres globaux
    /// </summary>
    public struct FlockReference : IComponentData
    {
        public Entity ManagerEntity;
    }

    /// <summary>
    /// Tag pour identifier les entités de type Flock
    /// </summary>
    public struct FlockTag : IComponentData
    {
    }

    // ============ COMPONENTS POUR LE MANAGER ============

    /// <summary>
    /// Paramètres globaux du flocking
    /// </summary>
    public struct FlockManagerSettings : IComponentData
    {
        public float MinSpeed;
        public float MaxSpeed;
        public float NeighbourDistance;
        public float RotationSpeed;
        public float DistanceBetweenFishes;
        public float3 SwimLimits;
        public float3 GoalPos;
        public int NumFish;
    }

    /// <summary>
    /// Limites de nage (Bounds)
    /// </summary>
    public struct FlockBounds : IComponentData
    {
        public float3 Center;
        public float3 Extents; // SwimLimits * 2
    }

    /// <summary>
    /// Authentification du manager DOTS
    /// </summary>
    public struct FlockManagerTag : IComponentData
    {
    }
}
