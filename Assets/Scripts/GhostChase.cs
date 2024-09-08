using UnityEngine;

public class GhostChase : GhostBehaviour
{

//    private void OnDisable()
//    {
//        ghost.scatter.Enable();
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        Node node = other.GetComponent<Node>();

//        if (node != null && this.enabled && !this.ghost.frightened.enabled)
//        {
//            Vector3 direction = Vector3.zero;
//            float minDistance = float.MaxValue;

//            foreach(Vector3 availableDirection in node.availableDirection)
//            {
//                Vector4 newPosition = this.transform.position  +  new Vector4(availableDirection.x, 0,  availableDirection.Z,);
//                float distance =(this.ghost.target.position - newPosition).sqrMagnitude;

//                if (distance < minDistance) {
//                    minDistance = distance;
//                    direction = availableDirection;
//                }

//                this.ghost.movement.SetDirection(direction);

//        }
//    }
}
