using System;
using UnityEngine;
using System.Collections;
using BulletSharp;

namespace BulletUnity
{
    [AddComponentMenu("Physics Bullet/Shapes/Cylinder")]
    public class BCylinderShape : BCollisionShape
    {
        [SerializeField] private Vector3 _halfExtent = new Vector3(0.5f, 1f, 0.5f);
        public Vector3 HalfExtent
        {
            get { return _halfExtent; }
            set
            {
                if (collisionShapePtr != null && value != _halfExtent)
                {
                    Debug.LogError("Cannot change the extents after the bullet shape has been created. Extents is only the initial value " +
                                    "Use LocalScaling to change the shape of a bullet shape.");
                }
                else {
                    _halfExtent = value;
                }
            }
        }

        [SerializeField] private Vector3 _localScaling = Vector3.one;
        public Vector3 LocalScaling
        {
            get { return _localScaling; }
            set
            {
                _localScaling = value;
                if (collisionShapePtr != null)
                {
                    ((CylinderShape)collisionShapePtr).LocalScaling = value.ToBullet();
                }
            }
        }

        public override void OnDrawGizmosSelected()
        {
            if (drawGizmo == false)
            {
                return;
            }
            Vector3 position = transform.position;
            Quaternion rotation = transform.rotation;
            Vector3 scale = _localScaling;
            BUtility.DebugDrawCylinder(position, rotation, scale, _halfExtent.x, _halfExtent.y, 1, Color.blue);
        }

        public override CollisionShape CopyCollisionShape()
        {
            var cs = new CylinderShape(_halfExtent.ToBullet()) {LocalScaling = _localScaling.ToBullet()};
            return cs;
        }

        public override CollisionShape GetCollisionShape()
        {
            if (collisionShapePtr != null) return collisionShapePtr;
            collisionShapePtr = new CylinderShape(_halfExtent.ToBullet());
            ((CylinderShape)collisionShapePtr).LocalScaling = _localScaling.ToBullet();
            return collisionShapePtr;
        }
    }
}