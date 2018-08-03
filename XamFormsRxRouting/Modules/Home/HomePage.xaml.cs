﻿using ReactiveUI;
using System.Reactive.Disposables;
using Xamarin.Forms;
using XamFormsRxRouting.Common;

namespace XamFormsRxRouting.Modules
{
	public partial class HomePage : BaseContentPage<IHomeViewModel>
	{
		public HomePage()
		{
			InitializeComponent();

            this.WhenActivated(
                disposables =>
                {
                    this
                        .BindCommand(ViewModel, vm => vm.PushPage, v => v.PushPageButton)
                        .DisposeWith(disposables);
                    this
                        .BindCommand(ViewModel, vm => vm.PushModalWithNav, v => v.PushModalWithNavButton)
                        .DisposeWith(disposables);
                    this
                        .BindCommand(ViewModel, vm => vm.PushModalWithoutNav, v => v.PushModalWithoutNavButton)
                        .DisposeWith(disposables);
                    this
                        .Bind(ViewModel, vm => vm.PopCount, v => v.PopCountEntry.Text)
                        .DisposeWith(disposables);
                    this
                        .BindCommand(ViewModel, vm => vm.PopPages, v => v.PopPagesButton)
                        .DisposeWith(disposables);
                    this
                        .Bind(ViewModel, vm => vm.PageIndex, v => v.PageIndexEntry.Text)
                        .DisposeWith(disposables);
                    this
                        .BindCommand(ViewModel, vm => vm.PopToNewPage, v => v.PopToNewPageButton)
                        .DisposeWith(disposables);
                });
        }
    }
}
