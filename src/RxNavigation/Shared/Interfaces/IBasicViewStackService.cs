using System;
using System.Collections.Immutable;
using System.Reactive;

namespace GameCtor.RxNavigation
{
    /// <summary>
    /// Interface that manages a stack of view models.
    /// </summary>
    public interface IBasicViewStackService
    {
        /// <summary>
        /// Gets the current view shell (platform-specific).
        /// </summary>
        IBasicViewShell ViewShell { get; }

        /// <summary>
        /// Gets the current stack.
        /// </summary>
        IObservable<IImmutableList<IViewModel>> Stack { get; }

        /// <summary>
        /// Pushes a View onto the current View stack.
        /// </summary>
        /// <param name="viewModel">A view model.</param>
        /// <param name="contract">A View contract.</param>
        /// <param name="resetStack">A flag signalling if the View stack should be reset.</param>
        /// <param name="animate">A flag signalling if the push should be animated.</param>
        /// <returns>An observable that signals the completion of this action.</returns>
        IObservable<Unit> Push(
            IViewModel viewModel,
            string contract = null,
            bool resetStack = false,
            bool animate = true);

        /// <summary>
        /// Inserts a View into the current View stack at the given index.
        /// </summary>
        /// <param name="index">An insertion index.</param>
        /// <param name="viewModel">A view model.</param>
        /// <param name="contract">A View contract.</param>
        void Insert(
            int index,
            IViewModel viewModel,
            string contract = null);

        /// <summary>
        /// Removes a view model from the stack at the given index.
        /// </summary>
        /// <param name="index">The index of the view model to remove.</param>
        void Remove(
            int index);

        /// <summary>
        /// Removes the given view model from the stack.
        /// </summary>
        /// <param name="viewModel">The view model to remove.</param>
        void Remove(
            IViewModel viewModel);

        /// <summary>
        /// Pops to the View at the given index.
        /// </summary>
        /// <param name="index">The index of the View to pop to.</param>
        /// <param name="animateLastView">A flag signalling if the final pop should be animated.</param>
        /// <returns>An observable that signals the completion of this action.</returns>
        IObservable<Unit> PopTo(
            int index,
            bool animateLastView = true);

        /// <summary>
        /// Pops the given number of Views from the top of the current View stack.
        /// </summary>
        /// <param name="count">The number of Views to pop.</param>
        /// <param name="animateLastView">A flag signalling if the final pop should be animated.</param>
        /// <returns>An observable that signals the completion of this action.</returns>
        IObservable<Unit> Pop(
            int count = 1,
            bool animateLastView = true);
    }
}
