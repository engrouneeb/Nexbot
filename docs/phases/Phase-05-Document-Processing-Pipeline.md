# Phase 5: Document Processing Pipeline (Week 5)

## Overview
Build the complete document ingestion and processing pipeline with background jobs.

## Objectives
- Create document upload endpoint
- Implement text extraction (PDF, DOCX, HTML)
- Build intelligent text chunking
- Set up Hangfire background processing
- Index documents into ChromaDB

## Key Components
- DocumentController
- TextExtractor (PDF, DOCX, HTML)
- TextChunker
- DocumentIndexingJob (Hangfire)

## Tasks
- [ ] Create upload endpoint with validation
- [ ] Install document processing libraries (iTextSharp, DocumentFormat.OpenXml, HtmlAgilityPack)
- [ ] Implement text extractors
- [ ] Build text chunking algorithm (500-1000 tokens, overlap)
- [ ] Install and configure Hangfire
- [ ] Create DocumentIndexingJob
- [ ] Add error handling and retry logic
- [ ] Test end-to-end pipeline

## Next Phase
[Phase 6: RAG & Chat API](./Phase-06-RAG-Chat-API.md)
