namespace dotnet_studies.Endpoints.User;

public record UserRequest(string Email, string Name, string Password, string SectorId);
