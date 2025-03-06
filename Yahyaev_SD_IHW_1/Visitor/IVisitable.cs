namespace Yahyaev_SD_IHW_1.Interfaces;

public interface IVisitable
{ 
    void Accept(IVisitor visitor);
}