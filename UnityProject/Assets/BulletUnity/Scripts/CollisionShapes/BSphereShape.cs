using System;
using UnityEngine;
using System.Collections;
using BulletSharp;

namespace BulletUnity {
    /// <summary>
    /// Class revised to match default Unity primitive along with minor code cleanup
    /// -VektorKnight
    /// </summary>
	[AddComponentMenu("Physics Bullet/Shapes/Sphere")]
    public class BSphereShape : BCollisionShape {
        [SerializeField] protected float _radius = 0.5f;
        public float Radius
        {
            get { return _radius; }
            set
            {
                if (collisionShapePtr != null && value != _radius)
                {
                    Debug.LogError("Cannot change the radius after the bullet shape has been created. Radius is only the initial value " +
                                    "Use LocalScaling to change the shape of a bullet shape.");
                }
                else {
                    _radius = value;
                }
            }
        }

        [SerializeField]
        protected Vector3 _localScaling = Vector3.one;
        public Vector3 LocalScaling
        {
            get { return _localScaling; }
            set
            {
                _localScaling = value;
                if (collisionShapePtr != null)
                {
                    ((SphereShape)collisionShapePtr).LocalScaling = value.ToBullet();
                }
            }
        }

        public override void OnDrawGizmosSelected() {
            if (drawGizmo == false)
            {
                return;
            }
            Vector3 position = transform.position;
            Quaternion rotation = transform.rotation;
            Vector3 scale = _localScaling;
            BUtility.DebugDrawSphere(position, rotation, scale, Vector3.one * _radius, Color.blue);
        }

        public override CollisionShape CopyCollisionShape()
        {
            var ss = new SphereShape(_radius) {LocalScaling = _localScaling.ToBullet()};
            return ss;
        }

        public override CollisionShape GetCollisionShape() {
            if (collisionShapePtr != null) return collisionShapePtr;
            collisionShapePtr = new SphereShape(_radius);
            ((SphereShape)collisionShapePtr).LocalScaling = _localScaling.ToBullet();
            return collisionShapePtr;
        }
    }
}
