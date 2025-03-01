using FluentAssertions;
using Restaurants.Domain.Constants;
using Xunit;


namespace Restaurants.Application.Users.Tests;

public class CurrentUserTests
{
    /// Naming Convetion :: TestMetod_Scenario_ExcpectedResult

    //[Fact()]
    [Theory()]
    [InlineData(RolesConstant.Admin)]
    [InlineData(RolesConstant.User)]
    public void IsInRole_WithMatchingRole_ShouldReturnTrue(string roleName)
    {
        // arrange
        var currentUser = new CurrentUser("1", "test@test.com", [RolesConstant.Admin, RolesConstant.User], null, null);
        // act
        var isInRole = currentUser.IsInRole(roleName);
        // assert
        isInRole.Should().BeTrue();
    }

    [Fact()]
    public void IsInRole_WithNoMatchingRole_ShouldReturnFalse()
    {
        // arrange
        var currentUser = new CurrentUser("1", "test@test.com", [RolesConstant.Admin, RolesConstant.User], null, null);
        // act
        var isInRole = currentUser.IsInRole(RolesConstant.Owner);
        // assert
        isInRole.Should().BeFalse();
    }

    [Fact()]
    public void IsInRole_WithMatchingRoleCase_ShouldReturnTrue()
    {
        // arrange
        var currentUser = new CurrentUser("1", "test@test.com", [RolesConstant.Admin, RolesConstant.User], null, null);
        // act
        var isInRole = currentUser.IsInRole(RolesConstant.Admin.ToLower());
        // assert
        isInRole.Should().BeFalse();
    }
}
