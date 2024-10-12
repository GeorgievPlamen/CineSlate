export function isProblemDetails(error: unknown): error is ProblemDetails {
  if (typeof error === 'object' && error !== null) {
    return (
      'status' in error &&
      'title' in error &&
      typeof (error as ProblemDetails).status === 'object' &&
      typeof (error as ProblemDetails).title === 'string'
    );
  }
  return false;
}

export interface ProblemDetails {
  status: HttpStatusCode;
  title: string;
  traceId: string;
  type: string;
  detail?: string;
}

export interface HttpStatusCode {
  code: number;
  text: string;
}
