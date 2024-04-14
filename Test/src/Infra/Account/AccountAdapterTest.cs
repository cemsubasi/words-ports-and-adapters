using Domain.Account.Entity;
using Domain.Account.UseCase;
using Infra.Account;
using Infra.Configurations;
using Infra.Context;
using Infra.src.main.Context.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;

namespace Infra.Test.Account;

public class AccountAdapterTest {
    private readonly Mock<MainDbContext> _mockContext;
    private readonly AccountAdapter _accountAdapter;
    private readonly Mock<DbSet<AccountEntity>> _mockSet;

    public AccountAdapterTest() {
        this._mockContext = new Mock<MainDbContext>();
        this._mockSet = new Mock<DbSet<AccountEntity>>();
        this._accountAdapter = new AccountAdapter(this._mockContext.Object, new Mock<JwtProvider>(new Mock<IOptions<JwtOptions>>().Object).Object);
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateAndSaveAccountEntity_WhenCalledWithValidData() {
        // arrange
        var useCase = AccountCreate.Build(
            name: "Sample Name",
            email: "sample@email.com",
            phone: "1234567890",
            password: "samplepassword");

        this._mockContext.Setup(m => m.Accounts).Returns(this._mockSet.Object);
        this._mockContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // act
        await this._accountAdapter.CreateAsync(useCase, CancellationToken.None);

        // assert
        this._mockSet.Verify(m => m.AddAsync(It.Is<AccountEntity>(a =>
            a.Name == useCase.Name &&
            a.Email == useCase.Email &&
            a.Phone == useCase.Phone &&
            a.Password == useCase.Password
        ), It.IsAny<CancellationToken>()), Times.Once);
    }
}