using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace GameCtor.RxNavigation
{
    /// <summary>
    /// Service that manages a stack of views.
    /// </summary>
    public sealed class BasicViewStackService : IBasicViewStackService
    {
        private readonly IBasicViewShell _viewShell;

        private BehaviorSubject<IImmutableList<IViewModel>> _currentStack;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicViewStackService"/> class.
        /// </summary>
        /// <param name="viewShell">The view shell (platform specific).</param>
        /// <param name="pages">A list of pages to initialize the page stack with.</param>
        public BasicViewStackService(IBasicViewShell viewShell, IList<IViewModel> pages = null)
        {
            _viewShell = viewShell ?? throw new NullReferenceException("The viewShell can't be null.");

            _currentStack = new BehaviorSubject<IImmutableList<IViewModel>>(ImmutableList<IViewModel>.Empty);

            var immutablePages = pages != null ? pages.ToImmutableList() : ImmutableList<IViewModel>.Empty;
            for (int i = 0; i < immutablePages.Count; ++i)
            {
                ViewShell.Insert(i, pages[i], null);
            }
        }

        /// <inheritdoc/>
        public IBasicViewShell ViewShell => _viewShell;

        /// <inheritdoc/>
        public IObservable<IImmutableList<IViewModel>> Stack => throw new NotImplementedException();

        /// <inheritdoc/>
        public void Insert(int index, IViewModel viewModel, string contract = null)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IObservable<Unit> Pop(int count = 1, bool animateLastView = true)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IObservable<Unit> PopTo(int index, bool animateLastView = true)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IObservable<Unit> Push(IViewModel viewModel, string contract = null, bool resetStack = false, bool animate = true)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Remove(int index)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Remove(IViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        private static void AddToStackAndTick<T>(BehaviorSubject<IImmutableList<T>> stackSubject, T item, bool reset)
        {
            var stack = stackSubject.Value;

            if (reset)
            {
                stack = new[] { item }.ToImmutableList();
            }
            else
            {
                stack = stack.Add(item);
            }

            stackSubject.OnNext(stack);
        }

        private static T PopStackAndTick<T>(BehaviorSubject<IImmutableList<T>> stackSubject)
        {
            var stack = stackSubject.Value;

            if (stack.Count == 0)
            {
                throw new InvalidOperationException("Stack is empty.");
            }

            var removedItem = stack[stack.Count - 1];
            stack = stack.RemoveAt(stack.Count - 1);
            stackSubject.OnNext(stack);
            return removedItem;
        }
    }
}
