namespace Domain.Models;

public class Constant : EntityBase
{
    public string? EntrepriseName { get; set; }

    public string? EntrepriseDescription { get; set; }

    public string? EntrepriseSlougant { get; set; }

    public string? EntreprisePhone { get; set; }

    public string? EntrepriseAdresse { get; set; }

    public string? EntrepriseFacebook { get; set; }
#nullable enable
#nullable disable
}

public class ConstantCreateDto
{
    public double CardPrice { get; set; }
    public double FirstCardPrice { get; set; }

    public string EntrepriseName { get; set; }

    public string EntrepriseDescription { get; set; }

    public string EntrepriseSlougant { get; set; }

    public string EntreprisePhone { get; set; }

    public string EntrepriseAdresse { get; set; }

    public string EntrepriseFacebook { get; set; }
}