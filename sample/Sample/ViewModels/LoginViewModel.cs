﻿using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Linq;
using GameCtor.RxNavigation;
using Sample.Common;

namespace Sample.Modules
{
    public class LoginViewModel : BaseViewModel, ILoginViewModel, IPageViewModel
    {
        private string _email;
        private string _password;

        public LoginViewModel(IViewStackService viewStackService)
            : base(viewStackService)
        {
            var canSignIn = this.WhenAnyValue(
                vm => vm.Email,
                vm => vm.Password,
                (email, password) => !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password));

            SignIn = ReactiveCommand.CreateFromObservable(
                () =>
                {
                    return Observable
                        .Timer(TimeSpan.FromSeconds(1))
                        .SelectMany(_ => ViewStackService.PushPage(new HomeViewModel(ViewStackService), null, true))
                        .TakeUntil(Cancel);
                },
                canSignIn);

            SignIn
                .ThrownExceptions
                .Subscribe(
                    x =>
                    {
                        System.Console.WriteLine(x);
                    });

            var canCancel = SignIn.IsExecuting;
            Cancel = ReactiveCommand.CreateFromObservable(
                () =>
                {
                    return Observable
                        .Return(Unit.Default);
                },
                canCancel);
        }

        public string Title => "Login";

        public string Email
        {
            get => _email;
            set => this.RaiseAndSetIfChanged(ref _email, value);
        }

        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        public ReactiveCommand<Unit, Unit> SignIn { get; }

        public ReactiveCommand<Unit, Unit> Cancel { get; }
    }
}
