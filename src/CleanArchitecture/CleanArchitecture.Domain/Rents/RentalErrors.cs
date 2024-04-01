namespace CleanArchitecture.Domain.Rents;

public static class RentalErrors
{
    public static Error NotFound = new Error(
        "Rental.Found",
        "The rental with the specified ID was not found."
    );

    public static Error Overlap = new Error(
        "Rental.Overlap",
        "The rental is booking for two o more customer at the same time."
    );

    public static Error NotReserved = new Error(
        "Rental.NotReserved",
        "The rental is not reserved."
    );

    public static Error NotConfirm = new Error(
        "Rental.NotConfirm",
        "The rental is not confirmed."
    );
    
    public static Error AlreadyStarted = new Error(
        "Rental.NotConfirm",
        "The rental is already started."
    );
    
}
