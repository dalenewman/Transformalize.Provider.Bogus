using System.Linq;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Transformalize.Configuration;
using Transformalize.Containers.Autofac;
using Transformalize.Contracts;
using Transformalize.Providers.Bogus.Autofac;
using Transformalize.Providers.Console;

namespace Tests.Unit {
   [TestClass]
   public class TestParallel {
      [TestMethod]
      public void ReadSerial() {
         const string xml = @"<add name='Bogus'>
  <connections>
    <add name='input' provider='bogus' seed='1' />
    <add name='output' provider='internal' />
  </connections>
  <entities>
    <add name='Bogus' alias='Contact' page='1' size='500000'>
      <fields>
        <add name='Identity' type='int' />
        <add name='FirstName' t='toLower().toUpper()' />
        <add name='LastName' t='replace(Newman,Noonan)' />
        <add name='Email' alias='Domain' length='100' t='split(@).last()' />
      </fields>
    </add>
  </entities>
</add>";
         var logger = new ConsoleLogger(LogLevel.Debug);
         using (var outer = new ConfigurationContainer().CreateScope(xml, logger)) {
            var process = outer.Resolve<Process>();
            using (var inner = new Container(new BogusModule()).CreateScope(process, logger)) {

               var controller = inner.Resolve<IProcessController>();
               Assert.AreEqual(1000000, controller.Read().Count());
            }
         }
      }

      [TestMethod]
      public void ReadParallel() {
         const string xml = @"<add name='Bogus' pipeline='parallel.linq'>
  <connections>
    <add name='input' provider='bogus' seed='1' />
    <add name='output' provider='internal' />
  </connections>
  <entities>
    <add name='Bogus' alias='Contact' page='1' size='500000' pipeline='parallel.linq'>
      <fields>
        <add name='Identity' type='int' />
        <add name='FirstName' t='toLower().toUpper()' />
        <add name='LastName' t='replace(Newman,Noonan)' />
        <add name='Email' alias='Domain' length='100' t='split(@).last()' />
      </fields>
    </add>
  </entities>
</add>";
         var logger = new ConsoleLogger(LogLevel.Debug);
         using (var outer = new ConfigurationContainer().CreateScope(xml, logger)) {
            var process = outer.Resolve<Process>();
            using (var inner = new Container(new BogusModule()).CreateScope(process, logger)) {

               var controller = inner.Resolve<IProcessController>();
               var count = controller.Read().Count();
               Assert.AreEqual(1000000, count);
            }
         }
      }
   }
}
