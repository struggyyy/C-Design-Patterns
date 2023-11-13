using System;
using System.Collections.Generic;

public abstract class ProductPrototype : ICloneable
{
    public decimal Price { get; protected set; }

    public ProductPrototype(decimal price)
    {
        Price = price;
    }

    public abstract object Clone();
}

public class Bread : ProductPrototype
{
    public Bread(decimal price) : base(price) { }

    public override object Clone()
    {
        return MemberwiseClone();
    }
}

public class Butter : ProductPrototype
{
    public Butter(decimal price) : base(price) { }

    public override object Clone()
    {
        return MemberwiseClone();
    }
}

public class Supermarket
{
    private Dictionary<string, ProductPrototype> _productList = new Dictionary<string, ProductPrototype>();

    public void AddProduct(string key, ProductPrototype productPrototype)
    {
        _productList.Add(key, productPrototype);
    }

    public ProductPrototype GetClonedProduct(string key)
    {
        if (_productList.ContainsKey(key))
        {
            return (ProductPrototype)_productList[key].Clone();
        }
        else
        {
            throw new ArgumentException("Product not found");
        }
    }
}

class MainClass
{
    public static void Main(string[] args)
    {
        Supermarket supermarket = new Supermarket();

        supermarket.AddProduct("Bread", new Bread(9.50m));
        supermarket.AddProduct("Butter", new Butter(5.30m));

        ProductPrototype product = supermarket.GetClonedProduct("Bread");
        Console.WriteLine(String.Format("Bread - {0:0.00} zł", product.Price));

        product = supermarket.GetClonedProduct("Butter");
        Console.WriteLine(String.Format("Butter - {0:0.00} zł", product.Price));
    }
}
