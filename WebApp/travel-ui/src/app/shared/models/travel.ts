export interface Travel {
  id: number;
  title: string;
  destination: string;
  description: string;
  price: number;
  durationDays: number;
  imageUrl?: string;
}