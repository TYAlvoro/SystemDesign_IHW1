using Yahyaev_SD_IHW_1.Interfaces;

namespace Yahyaev_SD_IHW_1.Domain;

public class Category : IVisitable
{
    public enum CategoryType { Income, Expense };
    
    public Guid Id { get; set; }
    public CategoryType Type { get; set; }
    public string Name { get; set; }

    public Category(CategoryType type, string name)
    {
        Id = Guid.NewGuid();
        Type = type;
        Name = name;
    }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}