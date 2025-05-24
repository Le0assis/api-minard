namespace minard_teste_2.models
{
    public class Dono
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Animal> Animais { get; set; }

        public Dono() { }

        public Dono(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
