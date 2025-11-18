// src/app/core/types/domain.ts

// Organização

export interface OrganizationDocument {
  number: string;
  type: string; // ex.: "CNPJ", "CPF"
}

export interface Organization {
  id: string;
  description: string;
  country: string;
  state: string;
  city: string;
  website?: string | null;
  email: string;
  whatsAppNumber: string;
  document: OrganizationDocument;
  settings: any; // JsonDocument
  isActive: boolean;
  createdAt: string;
  updatedAt?: string | null;
  deletedAt?: string | null;
}

// Role

export interface Role {
  id: string;
  name: string;
  description: string;
}

// Member

export interface Member {
  id: string;
  firstName: string;
  lastname: string;
  email: string;
  whatsAppNumber: string;
  isActive: boolean;
  createdAt: string;
  updatedAt?: string | null;
  deletedAt?: string | null;
}

// Campaign

export interface Campaign {
  id: string;
  name: string;
  description?: string | null;
  website?: string | null;
  checkoutSite?: string | null;
  status: string;
  organizationId: string;
  createdAt: string;
  updatedAt?: string | null;
  deletedAt?: string | null;
}

// VoucherTemplate

export interface VoucherTemplateContent {
  [key: string]: any;
}

export interface VoucherTemplate {
  id: string;
  category?: string | null;
  subtype?: string | null;
  content: VoucherTemplateContent;
  salesLimit: number | null;
  salesCount: number;
  price: number;
  currency: string;
  checkoutSite?: string | null;
  isActive: boolean;
  organizationId: string;
  campaignId: string;
  createdAt: string;
  updatedAt?: string | null;
  deletedAt?: string | null;
}

// VoucherInstance

export type VoucherStatus = 'Emitido' | 'Resgatado' | 'Cancelado' | 'Expirado';

export interface VoucherInstance {
  id: string;
  code: string;
  status: VoucherStatus;
  createdAt: string;
  canceled?: string | null;
  redeemedAt?: string | null;
  voucherTemplateId: string;
  saleId: string;
}

// Contexto do member

export interface MemberOrganizationContext extends Organization {
  role: Role;
}

export interface MemberMeDto {
  member: Member;
  organizations: MemberOrganizationContext[];
  campaigns: Campaign[];
  voucherTemplates: VoucherTemplate[];
  voucherInstances?: VoucherInstance[];
}

// Auth DTOs

export interface LoginRequest {
  username: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  expiresAt: string;
}
