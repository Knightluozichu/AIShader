using UnityEngine;
using UnityEngine.UI;

namespace UIFramework.Rendering
{
    /// <summary>
    /// 自定义UI渲染器
    /// </summary>
    [RequireComponent(typeof(CanvasRenderer))]
    public class CustomUIRenderer : UIRendererBase
    {
        private Mesh _mesh;
        private Vector3[] _vertices;
        private Vector2[] _uvs;
        private Color[] _colors;
        private int[] _triangles;

        protected override void Awake()
        {
            base.Awake();
            _mesh = new Mesh();
            _mesh.MarkDynamic();
        }

        public void SetMeshData(Vector3[] vertices, Vector2[] uvs, Color[] colors, int[] triangles)
        {
            _vertices = vertices;
            _uvs = uvs;
            _colors = colors;
            _triangles = triangles;
            _isDirty = true;
        }

        public override void UpdateRenderData()
        {
            if (_mesh == null || _vertices == null || _uvs == null || _colors == null || _triangles == null)
                return;

            _mesh.Clear();
            _mesh.vertices = _vertices;
            _mesh.uv = _uvs;
            _mesh.colors = _colors;
            _mesh.triangles = _triangles;
            _mesh.RecalculateNormals();
            _mesh.RecalculateBounds();

            if (_canvasRenderer != null)
            {
                _canvasRenderer.SetMesh(_mesh);
            }
        }

        protected override void OnDestroy()
        {
            if (_mesh != null)
            {
                Destroy(_mesh);
                _mesh = null;
            }
        }
    }
}