using Clean.Modules.Shared.Application.Interfaces.Messaging;
using FluentValidation;
using MediatR;
using NetArchTest.Rules;
using System.Reflection;
using Xunit;

namespace Clean.Modules.Shared.ArchTests.SeedWork;

public abstract partial class ApplicationTestsBase
{
    protected abstract Assembly ApplicationAssembly { get; }
    protected abstract Assembly DomainAssembly { get; }
    protected abstract Assembly InfrastructureAssembly { get; }

    [Fact]
    public void Command_ShouldBe_Immutable()
    {
        var types = Types.InAssembly(ApplicationAssembly)
            .That()
            .ImplementInterface(typeof(ICommand))
            .Or()
            .ImplementInterface(typeof(ICommand<>))
            .GetTypes();

        types.AssertAreImmutable();
    }

    [Fact]
    public void Query_ShouldBe_Immutable()
    {
        var types = Types.InAssembly(ApplicationAssembly)
            .That()
            .ImplementInterface(typeof(IQuery<>)).GetTypes();

        types.AssertAreImmutable();
    }

    [Fact]
    public void CommandHandler_ShouldHave_NameEndingWithCommandHandler()
    {
        var types = Types.InAssembly(ApplicationAssembly)
            .That()
            .ImplementInterface(typeof(ICommandHandler<>))
            .Or()
            .ImplementInterface(typeof(ICommandHandler<,>))
            .And()
            .DoNotHaveNameMatching(".*Decorator.*")
            .Should()
            .HaveNameEndingWith("CommandHandler")
            .GetResult()
            .FailingTypes;

        types.AssertFailingTypes();
    }

    [Fact]
    public void QueryHandler_ShouldHave_NameEndingWithQueryHandler()
    {
        var types = Types.InAssembly(ApplicationAssembly)
            .That()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .Should()
            .HaveNameEndingWith("QueryHandler")
            .GetResult()
            .FailingTypes;

        types.AssertFailingTypes();
    }

    [Fact]
    public void CommandAndQueryHandlers_ShouldNotBe_Public()
    {
        var types = Types.InAssembly(ApplicationAssembly)
            .That()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .Or()
            .ImplementInterface(typeof(ICommandHandler<>))
            .Or()
            .ImplementInterface(typeof(ICommandHandler<,>))
            .Should()
            .NotBePublic()
            .GetResult()
            .FailingTypes;

        types.AssertFailingTypes();
    }

    [Fact]
    public void Validator_ShouldHaveName_EndingWithValidator()
    {
        var types = Types.InAssembly(ApplicationAssembly)
            .That()
            .Inherit(typeof(AbstractValidator<>))
            .Should()
            .HaveNameEndingWith("Validator")
            .GetResult()
            .FailingTypes;

        types.AssertFailingTypes();
    }

    [Fact]
    public void Validators_ShouldNotBe_Public()
    {
        var types = Types.InAssembly(ApplicationAssembly)
            .That()
            .Inherit(typeof(AbstractValidator<>))
            .Should().NotBePublic().GetResult().FailingTypes;

        types.AssertFailingTypes();
    }

    //[Fact]
    //public void InternalCommand_ShouldHaveConstructorWith_JsonConstructorAttribute()
    //{
    //    var types = Types.InAssembly(ApplicationAssembly)
    //        .That()
    //        .Inherit(typeof(InternalCommandBase))
    //        .Or()
    //        .Inherit(typeof(InternalCommandBase<>))
    //        .GetTypes();

    //    var failingTypes = new List<Type>();

    //    foreach (var type in types)
    //    {
    //        bool hasJsonConstructorDefined = false;
    //        var constructors = type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
    //        foreach (var constructorInfo in constructors)
    //        {
    //            var jsonConstructorAttribute = constructorInfo.GetCustomAttributes(typeof(JsonConstructorAttribute), false);
    //            if (jsonConstructorAttribute.Length > 0)
    //            {
    //                hasJsonConstructorDefined = true;
    //                break;
    //            }
    //        }

    //        if (!hasJsonConstructorDefined)
    //        {
    //            failingTypes.Add(type);
    //        }
    //    }

    //    failingTypes.AssertFailingTypes();
    //}

    [Fact]
    public void MediatRRequestHandler_ShouldNotBe_UsedDirectly()
    {
        var types = Types.InAssembly(ApplicationAssembly)
            .That()
            .DoNotHaveName("ICommandHandler`1")
            .Should()
            .ImplementInterface(typeof(IRequestHandler<>))
            .GetTypes();

        List<Type> failingTypes = new List<Type>();
        foreach (var type in types)
        {
            bool isCommandHandler = type.GetInterfaces().Any(x =>
                x.IsGenericType &&
                x.GetGenericTypeDefinition() == typeof(ICommandHandler<>));
            bool isCommandWithResultHandler = type.GetInterfaces().Any(x =>
                x.IsGenericType &&
                x.GetGenericTypeDefinition() == typeof(ICommandHandler<,>));
            bool isQueryHandler = type.GetInterfaces().Any(x =>
                x.IsGenericType &&
                x.GetGenericTypeDefinition() == typeof(IQueryHandler<,>));
            if (!isCommandHandler && !isCommandWithResultHandler && !isQueryHandler)
            {
                failingTypes.Add(type);
            }
        }

        failingTypes.AssertFailingTypes();
    }

    [Fact]
    public void CommandWithResult_ShouldNot_ReturnUnit()
    {
        Type commandWithResultHandlerType = typeof(ICommandHandler<,>);
        IEnumerable<Type> types = Types.InAssembly(ApplicationAssembly)
            .That().ImplementInterface(commandWithResultHandlerType)
            .GetTypes().ToList();

        var failingTypes = new List<Type>();
        foreach (Type type in types)
        {
            var interfaceType = type.GetInterface(commandWithResultHandlerType.Name);
            if (interfaceType?.GenericTypeArguments[1] == typeof(Unit))
            {
                failingTypes.Add(type);
            }
        }

        failingTypes.AssertFailingTypes();
    }
}