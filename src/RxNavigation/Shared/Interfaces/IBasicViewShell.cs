using System;
using System.Reactive;

namespace GameCtor.RxNavigation
{
    /// <summary>
    /// Interface that manages a stack of views.
    /// </summary>
    public interface IBasicViewShell
    {
        /// <summary>
        /// Pushes a view model onto the current page stack.
        /// </summary>
        /// <param name="viewModel">A view model.</param>
        /// <param name="contract">A contract.</param>
        /// <param name="resetStack">A flag signalling if the stack should be reset.</param>
        /// <param name="animate">A flag signalling if the push should be animated.</param>
        /// <returns>An observable that signals the completion of this action.</returns>
        IObservable<Unit> Push(
            IViewModel viewModel,
            string contract,
            bool resetStack,
            bool animate);

        /// <summary>
        /// Pops a view model from the top of the stack.
        /// </summary>
        /// <param name="animate">Whether the pop should be animated or not.</param>
        /// <returns>An observable that signals the completion of this action.</returns>
        IObservable<Unit> Pop(
            bool animate);

        /// <summary>
        /// Inserts a view model into the stack at the given index.
        /// </summary>
        /// <param name="index">An insertion index.</param>
        /// <param name="page">A view model.</param>
        /// <param name="contract">A contract.</param>
        void Insert(
            int index,
            IViewModel page,
            string contract);

        /// <summary>
        /// Removes a view model from the stack at the given index.
        /// </summary>
        /// <param name="index">The index of the page to remove.</param>
        void Remove(int index);
    }
}
