import { Component, Injector } from '@angular/core';
import { createCustomElement } from '@angular/elements';
import { ChatbotWidgetComponent } from './chatbot-widget/chatbot-widget.component';

@Component({
  selector: 'app-root',
  standalone: true,
  template: '',
})
export class AppComponent {
  constructor(private injector: Injector) {
    const chatbotElement = createCustomElement(ChatbotWidgetComponent, { injector });
    
    if (!customElements.get('calimatic-chatbot-widget')) {
      customElements.define('calimatic-chatbot-widget', chatbotElement);
    }
  }
}
