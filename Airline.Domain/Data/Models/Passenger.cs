namespace Airline.Domain;

public class Passenger
{
    public required string FirstName {  get; set; }
    public required string LastName { get; set; }
    public string? Patronymic { get; set; }
    public required string PassportNumber { get; set; }
    public required DateOnly BirthDate {  get; set; }

}
