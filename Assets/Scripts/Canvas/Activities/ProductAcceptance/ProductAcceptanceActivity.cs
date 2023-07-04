using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductAcceptanceActivity : ActivityCanvas
{
    [SerializeField]
    private Transform _content;

    [SerializeField]
    private Transform _dragParent;

    [SerializeField]
    private Transform _dragDestination;

    [SerializeField]
    private List<Product> _products = new List<Product>();

    private List<Product> _generatedProducts = new List<Product>();

    private void Start()
    {
        GenerateProduct();
    }

    private void GenerateProduct()
    {
        if (_content.childCount > 0)
            return;

        var product = Instantiate(GetRandomProduct(), _content);
        product.Init(_dragParent, _dragDestination);
        product.OnDropProduct += GenerateProduct;
        _generatedProducts.Add(product);
    }

    private Product GetRandomProduct()
    {
        int minIndexRange = 0;
        int maxIndexRange = _products.Count;
        int randomIndex = Random.Range(minIndexRange, maxIndexRange);

        return _products[randomIndex];
    }
}
