using System.Reflection;

namespace Clean.Modules.Shared.ArchTests.SeedWork;

public abstract partial class ApplicationTestsBase
{
    protected abstract Assembly ApplicationAssembly { get; }

    protected abstract Assembly DomainAssembly { get; }

    protected abstract Assembly InfrastructureAssembly { get; }

    // TODO
    //[Fact]
    //public void Command_Should_Be_Immutable()
    //{
    //    var types = Types.InAssembly(ApplicationAssembly)
    //        .That()
    //        .Inherit(typeof(CommandBase))
    //        .Or()
    //        .Inherit(typeof(CommandBase<>))
    //        .Or()
    //        .Inherit(typeof(InternalCommandBase))
    //        .Or()
    //        .Inherit(typeof(InternalCommandBase<>))
    //        .Or()
    //        .ImplementInterface(typeof(ICommand))
    //        .Or()
    //        .ImplementInterface(typeof(ICommand<>))
    //        .GetTypes();

    //    types.AssertAreImmutable();
    //}

    //[Fact]
    //public void Query_Should_Be_Immutable()
    //{
    //    var types = Types.InAssembly(ApplicationAssembly)
    //        .That().ImplementInterface(typeof(IQuery<>)).GetTypes();

    //    types.AssertAreImmutable();
    //}

    //[Fact]
    //public void CommandHandler_Should_Have_Name_EndingWith_CommandHandler()
    //{
    //    var types = Types.InAssembly(ApplicationAssembly)
    //        .That()
    //        .ImplementInterface(typeof(ICommandHandler<>))
    //            .Or()
    //        .ImplementInterface(typeof(ICommandHandler<,>))
    //        .And()
    //        .DoNotHaveNameMatching(".*Decorator.*").Should()
    //        .HaveNameEndingWith("CommandHandler")
    //        .GetResult()
    //        .FailingTypes;

    //    types.AssertFailingTypes();
    //}

    //[Fact]
    //public void QueryHandler_Should_Have_Name_EndingWith_QueryHandler()
    //{
    //    var types = Types.InAssembly(ApplicationAssembly)
    //        .That()
    //        .ImplementInterface(typeof(IQueryHandler<,>))
    //        .Should()
    //        .HaveNameEndingWith("QueryHandler")
    //        .GetResult()
    //        .FailingTypes;

    //    types.AssertFailingTypes();
    //}

    //[Fact]
    //public void Command_And_Query_Handlers_Should_Not_Be_Public()
    //{
    //    var types = Types.InAssembly(ApplicationAssembly)
    //        .That()
    //            .ImplementInterface(typeof(IQueryHandler<,>))
    //                .Or()
    //            .ImplementInterface(typeof(ICommandHandler<>))
    //                .Or()
    //            .ImplementInterface(typeof(ICommandHandler<,>))
    //        .Should().NotBePublic().GetResult().FailingTypes;

    //    types.AssertFailingTypes();
    //}

    //[Fact]
    //public void Validator_Should_Have_Name_EndingWith_Validator()
    //{
    //    var types = Types.InAssembly(ApplicationAssembly)
    //        .That()
    //        .Inherit(typeof(AbstractValidator<>))
    //        .Should()
    //        .HaveNameEndingWith("Validator")
    //        .GetResult()
    //        .FailingTypes;

    //    types.AssertFailingTypes();
    //}

    //[Fact]
    //public void Validators_Should_Not_Be_Public()
    //{
    //    var types = Types.InAssembly(ApplicationAssembly)
    //        .That()
    //        .Inherit(typeof(AbstractValidator<>))
    //        .Should().NotBePublic().GetResult().FailingTypes;

    //    types.AssertFailingTypes();
    //}

    //[Fact]
    //public void InternalCommand_Should_Have_Constructor_With_JsonConstructorAttribute()
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

    //[Fact]
    //public void MediatR_RequestHandler_Should_NotBe_Used_Directly()
    //{
    //    var types = Types.InAssembly(ApplicationAssembly)
    //        .That().DoNotHaveName("ICommandHandler`1")
    //        .Should().ImplementInterface(typeof(IRequestHandler<>))
    //        .GetTypes();

    //    List<Type> failingTypes = new List<Type>();
    //    foreach (var type in types)
    //    {
    //        bool isCommandHandler = type.GetInterfaces().Any(x =>
    //            x.IsGenericType &&
    //            x.GetGenericTypeDefinition() == typeof(ICommandHandler<>));
    //        bool isCommandWithResultHandler = type.GetInterfaces().Any(x =>
    //            x.IsGenericType &&
    //            x.GetGenericTypeDefinition() == typeof(ICommandHandler<,>));
    //        bool isQueryHandler = type.GetInterfaces().Any(x =>
    //            x.IsGenericType &&
    //            x.GetGenericTypeDefinition() == typeof(IQueryHandler<,>));
    //        if (!isCommandHandler && !isCommandWithResultHandler && !isQueryHandler)
    //        {
    //            failingTypes.Add(type);
    //        }
    //    }

    //    failingTypes.AssertFailingTypes();
    //}

    //[Fact]
    //public void Command_With_Result_Should_Not_Return_Unit()
    //{
    //    Type commandWithResultHandlerType = typeof(ICommandHandler<,>);
    //    IEnumerable<Type> types = Types.InAssembly(ApplicationAssembly)
    //        .That().ImplementInterface(commandWithResultHandlerType)
    //        .GetTypes().ToList();

    //    var failingTypes = new List<Type>();
    //    foreach (Type type in types)
    //    {
    //        var interfaceType = type.GetInterface(commandWithResultHandlerType.Name);
    //        if (interfaceType?.GenericTypeArguments[1] == typeof(Unit))
    //        {
    //            failingTypes.Add(type);
    //        }
    //    }

    //    failingTypes.AssertFailingTypes();
    //}
}