using OSGeo.OGR;
using System;

namespace Poi.Util
{
    class GisUtil
    {
        private static string dbPath = Environment.CurrentDirectory + "\\poi.db";

        // 获取Geometry
        public static Geometry GetGeometry(int fid)
        {
            // 打开数据
            DataSource dataSource = Ogr.Open(dbPath, 0);
            Layer layer = dataSource.GetLayerByName("city");

            Feature feature = layer.GetFeature(fid);
            Geometry geometry = feature.GetGeometryRef();

            // 关闭数据
            layer.Dispose();

            return geometry;
        }

        // 获取外接矩形
        public static Envelope GetEnvelope(int fid)
        {
            Geometry geometry = GetGeometry(fid);
            Envelope envelope = new Envelope();
            geometry.GetEnvelope(envelope);

            return envelope;
        }

        // 判断 Geometry 是否包含点
        public static bool IsContainsPoint(Geometry geometry, double x, double y)
        {
            Geometry point = new Geometry(wkbGeometryType.wkbPoint);
            point.SetPoint(0, x, y, 0);

            return geometry.Contains(point);
        }

        // 生成矩形 Geometry
        public static Geometry GetEnvelopeGeometry(double minX, double maxX, double minY, double maxY)
        {
            string wkt = string.Format("POLYGON (({0} {2}, {0} {3}, {1} {3}, {1} {2}, {0} {2}))",
                    minX, maxX, minY, maxY);
            Geometry envelope = Geometry.CreateFromWkt(wkt);

            return envelope;
        }

        // 判读 Envelope 和 Geometry 的关系
        public static int GetEnvelopeRelationship(Geometry geometry, double minX, double maxX, double minY, double maxY)
        {
            Geometry envelope = GetEnvelopeGeometry(minX, maxX, minY, maxY);
            if (geometry.Intersect(envelope)) // 相交
            {
                if (geometry.Contains(envelope))
                {
                    return 2; // Geometry 包含 Envelope，接受所有 POI
                }
                return 1; // 需要进一步判断 POI 是否在 Geometry 内
            }

            return 0; // 相离，不用获取 POI
        }
    }
}
