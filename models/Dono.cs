namespace minard_teste_2.models
{
    public class Dono
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public ICollection<Animal>? Animais { get; set; }

        public Dono(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}
