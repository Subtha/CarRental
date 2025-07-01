namespace CarRental.Application.Validators
{
    public class ValidationResult
    {
        public bool IsValid => ErrorMessages.Count == 0;              
        public List<string> ErrorMessages { get; } = new List<string>();
    }
}
