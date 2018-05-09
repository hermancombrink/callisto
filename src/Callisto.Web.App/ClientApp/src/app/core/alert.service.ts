// ====================================================
// More Templates: https://www.ebenmonney.com/templates
// Email: support@ebenmonney.com
// ====================================================

import { Injectable } from '@angular/core';
import { HttpResponseBase } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Subject } from 'rxjs/Subject';
import { Utilities } from './utilities';
import { concat } from 'rxjs/operators';

@Injectable()
export class AlertService {

  private messages = new Subject<AlertMessage>();
  private stickyMessages = new Subject<AlertMessage>();
  private dialogs = new Subject<AlertDialog>();

  private _isLoading = false;
  private loadingMessageId: any;

  showDialog(message: string)
  showDialog(message: string, title?: string)
  showDialog(message: string, title?: string, severity?: MessageSeverity)
  showDialog(message: string, title?: string, severity?: MessageSeverity, okCallback?: (val?: any) => any)
  showDialog(message: string, title?: string, severity?: MessageSeverity, okCallback?: (val?: any) => any, cancelbtn?: boolean)
  showDialog(message: string, title?: string, severity?: MessageSeverity, okCallback?: (val?: any) => any, cancelbtn?: boolean, cancelCallback?: () => any)
  {
    title = title || '';
    severity = severity || MessageSeverity.default;

    this.dialogs.next({
      message: message,
      okCallback: okCallback,
      cancelCallback: cancelCallback,
      cancelbtn: cancelbtn,
      title: title,
      severity: severity
    });
  }


  showErrorMessage(message?: string) {
    if (!message) {
      message = 'That was not suppose to happen!';
    }
    this.showMessage('Oops!', message, MessageSeverity.error);
  }

  showWarningMessage(message?: string) {
    if (!message) {
      message = 'That was not suppose to happen!';
    }
    this.showMessage('Oops!', message, MessageSeverity.warn);
  }

  showSuccessMessage(message: string)
  showSuccessMessage(message: string, context?: string) {
    if (!context) {
      context = 'Callisto';
    }
    this.showMessage(context, message, MessageSeverity.success);
  }

  showInfoMessage(message: string)
  showInfoMessage(message: string, context?: string) {
    context = 'Callisto';

    this.showMessage(context, message, MessageSeverity.info);
  }

  showMessage(summary: string)
  showMessage(summary: string, detail: string, severity: MessageSeverity)
  showMessage(summaryAndDetails: string[], summaryAndDetailsSeparator: string, severity: MessageSeverity)
  showMessage(response: HttpResponseBase, ignoreValue_useNull: string, severity: MessageSeverity)
  showMessage(data: any, separatorOrDetail?: string, severity?: MessageSeverity) {

    if (!severity) {
      severity = MessageSeverity.default;
    }

    if (data instanceof HttpResponseBase) {
      data = Utilities.getHttpResponseMessage(data);
      separatorOrDetail = Utilities.captionAndMessageSeparator;
    }

    if (data instanceof Array) {
      for (const message of data) {
        const msgObject = Utilities.splitInTwo(message, separatorOrDetail);

        this.showMessageHelper(msgObject.firstPart, msgObject.secondPart, severity, false);
      }
    } else {
      this.showMessageHelper(data, separatorOrDetail, severity, false);
    }
  }


  showStickyMessage(summary: string)
  showStickyMessage(summary: string, detail: string, severity: MessageSeverity, error?: any)
  showStickyMessage(summaryAndDetails: string[], summaryAndDetailsSeparator: string, severity: MessageSeverity)
  showStickyMessage(response: HttpResponseBase, ignoreValue_useNull: string, severity: MessageSeverity)
  showStickyMessage(data: string | string[] | HttpResponseBase, separatorOrDetail?: string, severity?: MessageSeverity, error?: any) {

    if (!severity) {
      severity = MessageSeverity.default;
    }

    if (data instanceof HttpResponseBase) {
      data = Utilities.getHttpResponseMessage(data);
      separatorOrDetail = Utilities.captionAndMessageSeparator;
    }


    if (data instanceof Array) {
      for (const message of data) {
        const msgObject = Utilities.splitInTwo(message, separatorOrDetail);

        this.showMessageHelper(msgObject.firstPart, msgObject.secondPart, severity, true);
      }
    } else {

      if (error) {

        const msg = `Severity: '${MessageSeverity[severity]}', Summary: '${data}', Detail: '${separatorOrDetail}', Error: '${Utilities.safeStringify(error)}'`;

        switch (severity) {
          case MessageSeverity.default:
            this.logInfo(msg);
            break;
          case MessageSeverity.info:
            this.logInfo(msg);
            break;
          case MessageSeverity.success:
            this.logMessage(msg);
            break;
          case MessageSeverity.error:
            this.logError(msg);
            break;
          case MessageSeverity.warn:
            this.logWarning(msg);
            break;
          case MessageSeverity.wait:
            this.logTrace(msg);
            break;
        }
      }

      this.showMessageHelper(data, separatorOrDetail, severity, true);
    }
  }



  private showMessageHelper(summary: string, detail: string, severity: MessageSeverity, isSticky: boolean) {
    if (isSticky) {
      this.stickyMessages.next({ severity: severity, summary: summary, detail: detail });
    } else {
      this.messages.next({ severity: severity, summary: summary, detail: detail });
    }
  }



  startLoadingMessage(message = 'Loading...', caption = '') {
    this._isLoading = true;
    clearTimeout(this.loadingMessageId);

    this.loadingMessageId = setTimeout(() => {
      this.showStickyMessage(caption, message, MessageSeverity.wait);
    }, 1000);
  }

  stopLoadingMessage() {
    this._isLoading = false;
    clearTimeout(this.loadingMessageId);
    this.resetStickyMessage();
  }



  logDebug(msg) {
    console.debug(msg);
  }

  logError(msg) {
    console.error(msg);
  }

  logInfo(msg) {
    console.info(msg);
  }

  logMessage(msg) {
    console.log(msg);
  }

  logTrace(msg) {
    console.trace(msg);
  }

  logWarning(msg) {
    console.warn(msg);
  }




  resetStickyMessage() {
    this.stickyMessages.next();
  }




  getDialogEvent(): Observable<AlertDialog> {
    return this.dialogs.asObservable();
  }


  getMessageEvent(): Observable<AlertMessage> {
    return this.messages.asObservable();
  }

  getStickyMessageEvent(): Observable<AlertMessage> {
    return this.stickyMessages.asObservable();
  }



  get isLoadingInProgress(): boolean {
    return this._isLoading;
  }
}







// ******************** Dialog ******************** //
export class AlertDialog {
  constructor(public message: string, public okCallback: (val?: any) => any, public cancelCallback: () => any,
    public cancelbtn: boolean, public title: string, public severity: MessageSeverity) {

  }
}

export enum DialogType {
  alert,
  confirm,
  prompt
}
// ******************** End ******************** //






// ******************** Growls ******************** //
export class AlertMessage {
  constructor(public severity: MessageSeverity, public summary: string, public detail: string) { }
}

export enum MessageSeverity {
  default,
  info,
  success,
  error,
  warn,
  wait
}
// ******************** End ******************** //
