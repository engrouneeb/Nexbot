import { Component, OnInit, Input, ViewEncapsulation } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

interface Message {
  role: 'user' | 'assistant';
  content: string;
  timestamp: Date;
}

@Component({
  selector: 'calimatic-chatbot-widget',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './chatbot-widget.component.html',
  styleUrls: ['./chatbot-widget.component.scss'],
  encapsulation: ViewEncapsulation.ShadowDom
})
export class ChatbotWidgetComponent implements OnInit {
  @Input() botId: string = '';
  @Input() apiUrl: string = 'http://localhost:5000/api/public';
  @Input() primaryColor: string = '#6366f1';
  @Input() botName: string = 'Assistant';
  @Input() welcomeMessage: string = 'Hello! How can I help you today?';

  isOpen = false;
  messages: Message[] = [];
  userMessage = '';
  isLoading = false;

  ngOnInit(): void {
    console.log('🤖 CalimaticChatBot Widget initialized', this.botId);
    this.addWelcomeMessage();
  }

  addWelcomeMessage(): void {
    this.messages.push({
      role: 'assistant',
      content: this.welcomeMessage,
      timestamp: new Date()
    });
  }

  toggleChat(): void {
    this.isOpen = !this.isOpen;
  }

  closeChat(): void {
    this.isOpen = false;
  }

  async sendMessage(): Promise<void> {
    const message = this.userMessage.trim();
    if (!message || this.isLoading) return;

    this.messages.push({
      role: 'user',
      content: message,
      timestamp: new Date()
    });

    this.userMessage = '';
    this.isLoading = true;

    try {
      await new Promise(resolve => setTimeout(resolve, 1000));
      
      this.messages.push({
        role: 'assistant',
        content: `Thanks for your message: "${message}". API integration coming in Phase 6!`,
        timestamp: new Date()
      });
    } catch (error) {
      this.messages.push({
        role: 'assistant',
        content: 'Sorry, something went wrong. Please try again.',
        timestamp: new Date()
      });
    } finally {
      this.isLoading = false;
    }
  }

  onKeyPress(event: KeyboardEvent): void {
    if (event.key === 'Enter' && !event.shiftKey) {
      event.preventDefault();
      this.sendMessage();
    }
  }
}
