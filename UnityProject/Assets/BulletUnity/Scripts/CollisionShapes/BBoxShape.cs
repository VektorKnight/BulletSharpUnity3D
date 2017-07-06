using System;
using UnityEngine;
using System.Collections;
using BulletSharp;

namespace BulletUnity {
    /// <summary>
    /// Class revised to match default Unity primitive along with minor code cleanup
    /// -VektorKnight
    /// </summary>
	[AddComponentMenu("Physics Bullet/Shapes/Box")]
    public class BBoxShape : BCollisionShape {
        
	    //Default extents of the box primitive.
	    [SerializeField] private Vector3 _extents = new Vector3(0.5f, 0.5f, 0.5f);
        public Vector3 Extents {
            get { return _extents; }
            set {
                if (collisionShapePtr != null && value != _extents) {
                    Debug.LogError("Cannot change the extents after the bullet shape has been created. Extents is only the initial value " +
                                    "Use LocalScaling to change the shape of a bullet shape.");
                } else {
                    _extents = value;
                }
            }
        }
        
        //Local scaling of the shape
        [SerializeField] private Vector3 _localScaling = Vector3.one;
        public Vector3 LocalScaling {
            get { return _localScaling; }
            set {
                _localScaling = value;
                if (collisionShapePtr != null) {
                    ((BoxShape)collisionShapePtr).LocalScaling = value.ToBullet();
                }
            }
        }
        
        //Draw the shape in-editor for visualization
        public override void OnDrawGizmosSelected() {
            if (drawGizmo == false) return;
        
            Vector3 position = transform.position;
            Quaternion rotation = transform.rotation;
            Vector3 scale = _localScaling;
            BUtility.DebugDrawBox(position, rotation, scale, _extents, Color.blue);
        }
        
        public override CollisionShape CopyCollisionShape() {
            var bs = new BoxShape(_extents.ToBullet()) {LocalScaling = _localScaling.ToBullet()};
            return bs;
        }

        public override CollisionShape GetCollisionShape() {
            if (collisionShapePtr != null) return collisionShapePtr;
            collisionShapePtr = new BoxShape(_extents.ToBullet());
            ((BoxShape)collisionShapePtr).LocalScaling = _localScaling.ToBullet();
            return collisionShapePtr;
        }
    }
}
