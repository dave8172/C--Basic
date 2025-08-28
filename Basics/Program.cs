/*Problem 1: Sales Tax

Basic sales tax is applicable at a rate of 10% on all goods, except books, food,
and medical products that are exempt. Import duty is an additional sales tax
applicable on all imported goods at a rate of 5%, with no exemptions.

When I purchase items I receive a receipt which lists the name of all the items
and their price (including tax), finishing with the total cost of the items,
and the total amounts of sales taxes paid.  The rounding rules for sales tax are
that for a tax rate of n%, a shelf price of p contains (np/100 rounded up to
the nearest 0.05) amount of sales tax.

Write an application that prints out the receipt details for these shopping baskets...
INPUT:
Input 1:
1 book at 12.49
1 music CD at 14.99
1 chocolate bar at 0.85

Input 2:
1 imported box of chocolates at 10.00
1 imported bottle of perfume at 47.50

Input 3:
1 imported bottle of perfume at 27.99
1 bottle of perfume at 18.99
1 packet of headache pills at 9.75
1 box of imported chocolates at 11.25

Output 1
Output 1:
1 book: 12.49
1 music CD: 16.49
1 chocolate bar: 0.85
Sales Taxes: 1.50
Total: 29.83

Output 2:
1 imported box of chocolates: 10.50
1 imported bottle of perfume: 54.65
Sales Taxes: 7.65
Total: 65.15

Output 3:
1 imported bottle of perfume: 32.19
1 bottle of perfume: 20.89
1 packet of headache pills: 9.75
1 imported box of chocolates: 11.85
Sales Taxes: 6.70
Total: 74.68
*/


using System;
using System.Collections.Generic;
using System.Linq;

// Defines the tax categories for goods.
public enum TaxCategory
{
    Taxable,
    Exempt
}

// Represents a single item in a shopping basket.
public class Item
{
    public string Name { get; }
    public int Qty { get; }
    public decimal Price { get; }
    public bool IsImported { get; }
    public TaxCategory Category { get; }

    public Item(string name, int qty, decimal price, bool isImported, TaxCategory category)
    {
        Name = name;
        Qty = qty;
        Price = price;
        IsImported = isImported;
        Category = category;
    }

    //Sales Tax of 0.1 on all except food, medical and books. Extra 0.05 import duty for all
    public decimal GetSalesTax()
    {
        decimal taxRate = 0;

        if (Category == TaxCategory.Taxable)
        {
            taxRate += 0.10m; // Basic sales tax
        }

        if (IsImported)
        {
            taxRate += 0.05m; // Import duty
        }

        decimal rawTax = Price * Qty * taxRate;
        return RoundUpTax(rawTax);
    }

    private decimal RoundUpTax(decimal tax)
    {
        return Math.Ceiling(tax * 20) / 20; // Rounds up to the nearest 0.05
    }

    public decimal GetPriceWithTax()
    {
        return (Price * Qty) + GetSalesTax();
    }
}

// Represents a shopping basket containing multiple items.
public class ShoppingBasket
{
    public List<Item> Items { get; } = new List<Item>();

    public void AddItem(Item item)
    {
        Items.Add(item);
    }

    public decimal GetTotalSalesTax()
    {
        return Items.Sum(item => item.GetSalesTax());
    }

    public decimal GetTotalCost()
    {
        return Items.Sum(item => item.GetPriceWithTax());
    }
}

// Handles the printing of the receipt.
public class ReceiptPrinter
{
    public void PrintReceipt(ShoppingBasket basket)
    {
        foreach (var item in basket.Items)
        {
            Console.WriteLine($"{item.Qty} {item.Name}: {item.GetPriceWithTax():F2}"); //F2 for limiting to 2 decimal places
        }

        Console.WriteLine($"Sales Taxes: {basket.GetTotalSalesTax():F2}");
        Console.WriteLine($"Total: {basket.GetTotalCost():F2}");
        Console.WriteLine();
    }
}

// Main class to run the application.
public class Program
{
    static void Main(string[] args)
    {
        // Basket 1
        //Each item has variables -> type, qty, price, isImported(bool), Tax category either Taxable or Exempt
        var basket1 = new ShoppingBasket();
        basket1.AddItem(new Item("book", 1, 12.49m, false, TaxCategory.Exempt));
        basket1.AddItem(new Item("music CD", 1, 14.99m, false, TaxCategory.Taxable));
        basket1.AddItem(new Item("chocolate bar", 1, 0.85m, false, TaxCategory.Exempt));

        var printer = new ReceiptPrinter();
        printer.PrintReceipt(basket1);

        // Basket 2
        var basket2 = new ShoppingBasket();
        basket2.AddItem(new Item("imported box of chocolates", 1, 10.00m, true, TaxCategory.Exempt));
        basket2.AddItem(new Item("imported bottle of perfume", 1, 47.50m, true, TaxCategory.Taxable));

        printer.PrintReceipt(basket2);

        // Basket 3
        var basket3 = new ShoppingBasket();
        basket3.AddItem(new Item("imported bottle of perfume", 1, 27.99m, true, TaxCategory.Taxable));
        basket3.AddItem(new Item("bottle of perfume", 1, 18.99m, false, TaxCategory.Taxable));
        basket3.AddItem(new Item("packet of headache pills", 1, 9.75m, false, TaxCategory.Exempt));
        basket3.AddItem(new Item("box of imported chocolates", 1, 11.25m, true, TaxCategory.Exempt));

        printer.PrintReceipt(basket3);
    }
}

/* Next step of improving this would be having user input instead of hard coding input, and deducting
TaxCategory and isImport from the input itself. */