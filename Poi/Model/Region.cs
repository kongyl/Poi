namespace Poi.Model
{
    class Region
    {
        public object Id { get; set; }
        
        public string Name { get; set; }

        public object ParentId { get; set; }

        // 用于ComboBox的显示
        public override string ToString()
        {
            return Name;
        }
    }
}
