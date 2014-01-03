using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlanPolicy.Model;

namespace PlanPolicyTest
{
    [TestClass]
    public class PlanPolicyTest
    {
        [TestMethod]
        public void PlanCreationDoesNotExceedServerResources()
        {
            var plan = Plan<Metrics>.Create(MetricType.CPU, 0.25d);
            Assert.IsTrue(plan.IsValid());
        }

        [TestMethod]
        public void UsersAreAssignedToGroups()
        {
            var user = User.Create("foo");
            Assert.IsTrue(user.Group != null);
        }

        [TestMethod]
        public void PoliciesAssignGroupsToPlans()
        {
            var user = User.Create("foo");
            var plan = Policy.Classify(user.Group);
            Assert.IsTrue(plan.IsValid());
        }
    }
}
