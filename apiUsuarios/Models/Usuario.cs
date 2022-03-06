namespace Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaAlta { get; set; }
        public Usuario(int Id, string Email, string Password, bool Activo, DateTime FechaAlta)
        {
            this.Id = Id;
            this.Email = Email;
            this.Password = Password;
            this.Activo = Activo;
            this.FechaAlta = FechaAlta;
        }
    }
}