namespace minard_teste_2.models
{
    public class Animal
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public string Tipo { get; set; }

        //Chave estrangeira
        public int DonoId { get; set; }
        public Dono Dono { get; set; }

        public Animal() { }

        public Animal(int id, string nome, string tipo, Dono dono) {
            this.Id = id;
            this.Nome = nome;
            this.Tipo = tipo;
            this.Dono = dono;
        }
    }
}
