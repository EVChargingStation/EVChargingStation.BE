namespace EVChargingStation.Domain.Enums;

public enum InvoiceStatus
{
    Draft = 0,
    Issued = 1,
    Paid = 2,
    Overdue = 3,
    Void = 4
}