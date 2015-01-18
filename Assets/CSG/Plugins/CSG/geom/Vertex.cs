using UnityEngine;

namespace ConstructiveSolidGeometry
{
    /// <summary>
    /// Represents a vertex of a polygon. Use your own vertex class instead of this
    /// one to provide additional features like texture coordinates and vertex
    /// colors. Custom vertex classes need to implement the IVertex interface.
    /// 
    /// </summary>
    public class Vertex : IVertex
    {
        public Vector3 pos { get; set; }
        public Vector3 normal;
        // TODO: Memomry optimization - this could probably be a short rather than int, 
        //       accessed as a get/set int property that converts the -32,768 to 32,767 range to 0 to 65535
        public int index = -1;

        public Vertex(Vector3 pos)
        {
            this.pos = pos;
            //this.normal = (this.normal != Vector3.zero) ? normal : Vector3.zero;
        }

        public Vertex(Vector3 pos, Vector3 normal)
        {
            this.pos = pos;
            this.normal = normal;
        }

        public IVertex clone()
        {
            return new Vertex(this.pos, this.normal);
        }

        public void flip()
        {
            this.normal *= -1f;
        }

        public IVertex Leap(IVertex other, float t)
        {
            return new Vertex(
                Vector3.Lerp(this.pos, other.pos, t),
				Vector3.Lerp(this.normal, (other as Vertex).normal, t)
            );

        }
    }
}