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

interface HttpStatusCode {
  code: number;
  text: string;
}

export function getErrorDetails(error: ProblemDetails) {
  switch (error.status.code) {
    case 400:
      if (error.detail) return error.detail;
      return error.status.text;
    case 404:
      return error.detail;
    default:
      return error.status.text;
  }
}
