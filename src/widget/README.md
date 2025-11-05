# Embeddable Chatbot Widget

A lightweight, vanilla JavaScript widget that can be embedded on any website.

## Features
- Zero dependencies
- < 50KB minified
- Cross-browser compatible
- Mobile responsive
- Customizable appearance
- Supports lead capture

## Usage

### Basic Embed
```html
<script src="https://your-platform.com/widget/chatbot-widget.js" 
        data-bot-id="YOUR_BOT_ID" 
        defer>
</script>
```

### Advanced Configuration
```html
<script src="https://your-platform.com/widget/chatbot-widget.js" 
        data-bot-id="YOUR_BOT_ID"
        data-api-url="https://api.your-platform.com"
        data-position="bottom-right"
        data-theme="light"
        defer>
</script>
```

## Development

### File Structure
```
widget/
├── chatbot-widget.js       # Main widget script
├── styles.css              # Widget styles
├── index.html              # Test page
└── README.md               # This file
```

### Testing Locally
```bash
cd src/widget
python -m http.server 3000
```

Visit `http://localhost:3000` to test the widget.

## Configuration Options

| Attribute | Type | Default | Description |
|-----------|------|---------|-------------|
| `data-bot-id` | string | **required** | Your chatbot ID |
| `data-api-url` | string | Current domain | API endpoint URL |
| `data-position` | string | `bottom-right` | Widget position: `bottom-right`, `bottom-left` |
| `data-theme` | string | `light` | Theme: `light`, `dark`, `auto` |
| `data-primary-color` | string | From bot config | Custom primary color (hex) |

## Browser Support
- Chrome 90+
- Firefox 88+
- Safari 14+
- Edge 90+

## Implementation Details

See [Phase 7: Public API & Widget](../../docs/phases/Phase-07-Public-API-Widget.md) for implementation guide.
