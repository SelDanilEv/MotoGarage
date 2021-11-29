export interface ErrorResponse {
  status: number;
  title: string;
  type: string;
  errors: Map<string, Array<string>>;
}
