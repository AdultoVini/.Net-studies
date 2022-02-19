namespace IWantApp.Endpoints.Employee;

public record EmployeeRequest(string Email, string Password, string Name, string EmployeeCode);
