using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ProductClassificationSystem
{
    public partial class MainWindow : Window
    {
        private Stack<UIElement> NavigationStack = new Stack<UIElement>();
        private List<Category> Categories = new List<Category>();

        public MainWindow()
        {
            InitializeComponent();
            InitializeCategories();
            ShowMainMenu();
        }

        private void InitializeCategories()
        {
            // Пример данных для категорий, подкатегорий и товаров
            Categories.Add(new Category("Электроника", new List<SubCategory>
            {
                new SubCategory("Мобильные телефоны", new List<Product>
                {
                    new Product("iPhone 14", "Apple", "Последняя модель iPhone."),
                    new Product("Samsung Galaxy S23", "Samsung", "Флагманский телефон на Android.")
                }),
                new SubCategory("Ноутбуки", new List<Product>
                {
                    new Product("MacBook Pro", "Apple", "Мощный ноутбук для профессионалов."),
                    new Product("Dell XPS 13", "Dell", "Компактный и мощный ноутбук.")
                })
            }));

            Categories.Add(new Category("Бытовая техника", new List<SubCategory>
            {
                new SubCategory("Холодильники", new List<Product>
                {
                    new Product("LG Умный холодильник", "LG", "Энергоэффективный холодильник с умными функциями."),
                    new Product("Whirlpool Двухдверный", "Whirlpool", "Просторный и надежный холодильник.")
                }),
                new SubCategory("Стиральные машины", new List<Product>
                {
                    new Product("Bosch Фронтальная загрузка", "Bosch", "Тихая и эффективная стиральная машина."),
                    new Product("Samsung Вертикальная загрузка", "Samsung", "Доступная и надежная стиральная машина.")
                })
            }));
        }

        private void ShowMainMenu()
        {
            NavigationStack.Clear();
            var mainMenu = new StackPanel();

            foreach (var category in Categories)
            {
                var button = new Button { Content = category.Name, Margin = new Thickness(5) };
                button.Click += (s, e) => ShowCategory(category);
                mainMenu.Children.Add(button);
            }

            mainMenu.Children.Add(CreateBackButton());
            NavigationStack.Push(mainMenu);
            Content = mainMenu;
        }

        private void ShowCategory(Category category)
        {
            var categoryPanel = new StackPanel();

            foreach (var subCategory in category.SubCategories)
            {
                var button = new Button { Content = subCategory.Name, Margin = new Thickness(5) };
                button.Click += (s, e) => ShowSubCategory(subCategory);
                categoryPanel.Children.Add(button);
            }

            categoryPanel.Children.Add(CreateBackButton());
            NavigationStack.Push(categoryPanel);
            Content = categoryPanel;
        }

        private void ShowSubCategory(SubCategory subCategory)
        {
            var subCategoryPanel = new StackPanel();

            foreach (var product in subCategory.Products)
            {
                var button = new Button { Content = product.Name, Margin = new Thickness(5) };
                button.Click += (s, e) => ShowProductDetails(product);
                subCategoryPanel.Children.Add(button);
            }

            subCategoryPanel.Children.Add(CreateBackButton());
            NavigationStack.Push(subCategoryPanel);
            Content = subCategoryPanel;
        }

        private void ShowProductDetails(Product product)
        {
            var detailsPanel = new StackPanel();

            detailsPanel.Children.Add(new TextBlock { Text = $"Название: {product.Name}", Margin = new Thickness(5) });
            detailsPanel.Children.Add(new TextBlock { Text = $"Бренд: {product.Brand}", Margin = new Thickness(5) });
            detailsPanel.Children.Add(new TextBlock { Text = $"Описание: {product.Description}", Margin = new Thickness(5) });
            detailsPanel.Children.Add(CreateBackButton());

            NavigationStack.Push(detailsPanel);
            Content = detailsPanel;
        }

        private Button CreateBackButton()
        {
            var backButton = new Button { Content = "Назад", Margin = new Thickness(5) };
            backButton.Click += (s, e) => GoBack();
            return backButton;
        }

        private void GoBack()
        {
            if (NavigationStack.Count > 1)
            {
                NavigationStack.Pop();
                Content = NavigationStack.Peek();
            }
        }
    }

    public class Category
    {
        public string Name { get; }
        public List<SubCategory> SubCategories { get; }

        public Category(string name, List<SubCategory> subCategories)
        {
            Name = name;
            SubCategories = subCategories;
        }
    }

    public class SubCategory
    {
        public string Name { get; }
        public List<Product> Products { get; }

        public SubCategory(string name, List<Product> products)
        {
            Name = name;
            Products = products;
        }
    }

    public class Product
    {
        public string Name { get; }
        public string Brand { get; }
        public string Description { get; }

        public Product(string name, string brand, string description)
        {
            Name = name;
            Brand = brand;
            Description = description;
        }
    }
}
