namespace E_commerce.Models;

// one number of a vector with the category name, used for displaying vectors
public class VectorValue
{
    public int IdProductType { get; set; }
    public string TypeName { get; set; }
    public double Weight { get; set; }
    public double LastChange { get; set; }
}
