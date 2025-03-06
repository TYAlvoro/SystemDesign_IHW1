namespace Yahyaev_SD_IHW_1.Observer;

public interface IBankAccountObserver
{
    void HandleBalanceChanged(object sender, EventArgs e);
}