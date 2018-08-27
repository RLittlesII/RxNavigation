﻿using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using GameCtor.RxNavigation;

namespace Sample.Native.iOS
{
    public class HomeViewModel : BaseViewModel, IPageViewModel
    {
        private int? _popCount = 1;
        private int? _pageIndex;
        private ObservableAsPropertyHelper<int> _pageCount;

        public HomeViewModel(IViewStackService viewStackService)
            : base(viewStackService)
        {
            PushPage = ReactiveCommand.CreateFromObservable(
                () =>
                {
                    return ViewStackService.PushPage(new HomeViewModel(ViewStackService));
                });

            PushModalWithNav = ReactiveCommand.CreateFromObservable(
                () =>
                {
                    return ViewStackService.PushModal(new NavigationPageViewModel(new HomeViewModel(ViewStackService)));
                });

            PushModalWithoutNav = ReactiveCommand.CreateFromObservable(
                () =>
                {
                    return ViewStackService.PushModal(new HomeViewModel(ViewStackService));
                });

            PopModal = ReactiveCommand.CreateFromObservable(
                () =>
                {
                    return ViewStackService.PopModal();
                });

            var canPop = this.WhenAnyValue(
                vm => vm.PopCount,
                vm => vm.PageCount,
                (popCount, pageCount) => popCount > 0 && popCount < pageCount);

            PopPages = ReactiveCommand.CreateFromObservable(
                () =>
                {
                    return ViewStackService
                        .PopPages(_popCount ?? 0, true);
                },
                canPop);

            var canPopToNewPage = this.WhenAnyValue(
                vm => vm.PageIndex,
                pageIndex => pageIndex >= 0 && pageIndex < PageCount);

            PopToNewPage = ReactiveCommand.CreateFromObservable(
                () =>
                {
                    return Observable
                        .Start(() => ViewStackService.InsertPage(PageIndex ?? 0, new LoginViewModel(ViewStackService)), RxApp.MainThreadScheduler)
                        .Concat(ViewStackService.PopToPage(PageIndex ?? 0));
                },
                canPopToNewPage);

            this.WhenActivated(
                disposables =>
                {
                    _pageCount = ViewStackService
                        .PageStack
                        .Select(
                            x =>
                            {
                                return x != null ? x.Count : 0;
                            })
                        .ToProperty(this, vm => vm.PageCount, default(int), false, RxApp.MainThreadScheduler)
                        .DisposeWith(disposables);
                });
        }

        public string Title => nameof(HomeViewModel);

        public int? PopCount
        {
            get => _popCount;
            set => this.RaiseAndSetIfChanged(ref _popCount, value);
        }

        public int? PageIndex
        {
            get => _pageIndex;
            set => this.RaiseAndSetIfChanged(ref _pageIndex, value);
        }

        public int PageCount => _pageCount != null ? _pageCount.Value : 0;

        public ReactiveCommand PushPage { get; }

        public ReactiveCommand PushModalWithNav { get; }

        public ReactiveCommand PushModalWithoutNav { get; }

        public ReactiveCommand PopModal { get; }

        public ReactiveCommand<Unit, Unit> PopPages { get; set; }

        public ReactiveCommand<Unit, Unit> PopToNewPage { get; set; }
    }
}
