using Infra.Category.Adapter;
using Infra.Context;
using Infra.Test.Integrations.Configuration;
using Microsoft.Extensions.Configuration;

namespace Infra.Test.Category.Integrations;

public class CategoryAdapterTest {
    private readonly MainDbContext context;
    public CategoryAdapterTest() {
        this.context = new MainDbContext(Test.Integrations.Configuration.ConfigurationProvider.Instance.GetSection("ConnectionStrings").GetValue<string>("MainDb"));
    }

    [Fact]
    public async Task FindOrCreate_ShouldReturnSameId_WhenPassSameCategoryName() {
        // Arrange
        var categoryAdapter = new CategoryAdapter(this.context);

        // Act
        var existingCategory = await categoryAdapter.FindOrCreateCategoryAsync("existing-category", CancellationToken.None);
        var newCategory = await categoryAdapter.FindOrCreateCategoryAsync("existing-category", CancellationToken.None);

        // Assert
        Assert.Equal(existingCategory.Id, newCategory.Id);

        // Clean
        await this.context.DisposeAsync();
    }
}