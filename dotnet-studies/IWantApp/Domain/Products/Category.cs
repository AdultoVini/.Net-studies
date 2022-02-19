using Flunt.Validations;

namespace IWantApp.Domain.Products;

public class Category : Entity
{
    public string Name { get; private set; }
    public bool Active { get; private set; }

    public Category(string name, string createdBy)
    {
        Name = name;
        Active = true;
        CreatedOn = DateTime.Now;
        CreatedBy = createdBy;
        EditedOn = DateTime.Now;
        EditedBy = createdBy;

        Validate();
    }

    public void Validate()
    {
        var contract = new Contract<Category>()
            .IsNotNullOrWhiteSpace(Name, "Name", "Nome da categoria é obrigatório!")
            .IsGreaterOrEqualsThan(Name, 3, "Name", "Nome precisa ser maior que 2 caractéres")
            .IsNotNullOrWhiteSpace(CreatedBy, "CreatedBy", "Nome do autor é obrigatório!")
            .IsNotNullOrWhiteSpace(EditedBy, "EditedBy", "Nome do autor que editou é obrigatório!");
        AddNotifications(contract);
    }
    public void EditInfo(string name, string editedBy, bool active)
    {
        Name = name;
        Active = true;
        EditedOn = DateTime.Now;
        EditedBy = editedBy;
        Validate();
    }


}
