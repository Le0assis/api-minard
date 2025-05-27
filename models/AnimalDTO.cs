namespace minard_teste_2.models
{
    public class AnimalDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public int DonoId { get; set; }
        public DonoDTO Dono { get; set; } 
    }
}
