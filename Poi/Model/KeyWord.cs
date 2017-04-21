namespace Poi.Model
{
    class KeyWord
    {
        public object Id { get; set; }

        public string Name { get; set; }

        public object ParentId { get; set; }

        public object Type { get; set; }

        // 在ComboBox中显示
        public override string ToString()
        {
            return Name;
        }
    }
}
