using System.IO;
using System.Net;
using DasBlog.Tests.Support;
using DasBlog.Tests.Support.Common;
using DasBlog.Tests.Support.Interfaces;
using Xunit;
using Xunit.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Constants = DasBlog.Tests.Support.Common.Constants;

namespace DasBlog.Tests.FunctionalTests.TestInfrastructureTests
{
	public class GitVersionedFileServiceTests : IClassFixture<InfrastructureTestPlatform>
	{
		private readonly InfrastructureTestPlatform platform;

		public GitVersionedFileServiceTests(ITestOutputHelper testOutputHelper, InfrastructureTestPlatform platform)
		{
			this.platform = platform;
			this.platform.CompleteSetup(testOutputHelper);
		}

		[Fact]
		[Trait(Constants.CategoryTraitType, Constants.TestInfrastructureTestTraitValue)]
		public void GitFS_ForUnmidifiedDirectory_ReturnsClean()
		{
			var gitfs = platform.ServiceProvider.GetService<IVersionedFileService>();
			(var clean, var errorMessage) = gitfs.IsClean();
			Assert.True(clean);
		}

		[Fact]
		[Trait(Constants.CategoryTraitType, Constants.TestInfrastructureTestTraitValue)]
		public void GitFs_ForModifiedDirectory_ReturnsNotClean()
		{
			var testPath = Path.Combine(Utils.GetProjectRootDirectory(), Constants.VanillaTestData, "aaa");
			try
			{
				File.WriteAllText(testPath, "aaa");
				var gitfs = platform.ServiceProvider.GetService<IVersionedFileService>();
				(var clean, var errorMessage) = gitfs.IsClean();
				Assert.False(clean);
			}
			finally
			{
				File.Delete(testPath);
			}
		}

		[Fact]
		[Trait(Constants.CategoryTraitType, Constants.TestInfrastructureTestTraitValue)]
		public void GitVersionedFS_IfGitIsInstalled_IsActive()
		{
			var gitfs = platform.ServiceProvider.GetService<IVersionedFileService>();
			Assert.True(gitfs.IsActive().active);
		}
	}
}
