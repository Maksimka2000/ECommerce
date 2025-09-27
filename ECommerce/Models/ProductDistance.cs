using E_commerce.Entities;

namespace E_commerce.Models;

// result of the Euclidean distance between the user vector and one product vector
public class ProductDistance
{
    public Product Product { get; set; }
    public List<VectorValue> ProductVector { get; set; }
    public double SumOfSquares { get; set; }
    public double Distance { get; set; }
}
