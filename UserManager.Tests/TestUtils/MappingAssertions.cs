using AutoMapper;
using FluentAssertions.Collections;
using FluentAssertions.Primitives;

namespace UserManager.Tests.TestUtils
{
    public static class MappingAssertions
    {
        /// <summary>
        /// Asserts that an object is equivalent to another mapped object.
        /// </summary>
        /// <typeparam name="TMapped"></typeparam>
        /// <param name="assertions"></param>
        /// <param name="expectation"></param>
        /// <param name="mapper"></param>
        public static void BeEquivalentToMapped<TMapped>(this ObjectAssertions assertions, object expectation, IMapper mapper)
        {
            var mappedExpectation = mapper.Map<TMapped>(expectation);
            assertions.BeEquivalentTo(mappedExpectation);
        }

        /// <summary>
        /// Asserts that a collection of objects contains at least one object equivalent to another mapped object.
        /// </summary>
        /// <typeparam name="TMapped"></typeparam>
        /// <param name="assertions"></param>
        /// <param name="expectation"></param>
        /// <param name="mapper"></param>
        public static void ContainEquivalentOfMapped<TMapped>(
            this GenericCollectionAssertions<TMapped> assertions,
            object expectation,
            IMapper mapper)
        {
            var mappedExpectation = mapper.Map<TMapped>(expectation);
            assertions.ContainEquivalentOf(mappedExpectation);
        }
    }
}
